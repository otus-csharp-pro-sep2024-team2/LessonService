using AutoMapper;
using LessonService.Commands.Commands.Handlers;
using LessonService.Commands.Commands.Request;
using LessonService.Domain.Entities;
using LessonService.Domain.Models.Lesson;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace LessonService.Tests;
public class DeleteLessonCommandHandlerTests
{
    private readonly Mock<AppDbContext> _contextMock;
    private readonly Mock<ILessonServiceApp> _lessonServiceAppMock;
    private readonly Mock<ILogger<DeleteLessonCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly DeleteLessonCommandHandler _handler;

    public DeleteLessonCommandHandlerTests()
    {
        _contextMock = new Mock<AppDbContext>();
        _lessonServiceAppMock = new Mock<ILessonServiceApp>();
        _loggerMock = new Mock<ILogger<DeleteLessonCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        
        _handler = new DeleteLessonCommandHandler(
            _contextMock.Object,
            _lessonServiceAppMock.Object,
            _loggerMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Delete_Lesson_And_Return_Response()
    {
        // Arrange
        var lessonId = Guid.NewGuid();
        var lesson = new Lesson { Id = lessonId };
        var command = new DeleteLessonCommand(lessonId);
        var cancellationToken = new CancellationToken();

        _lessonServiceAppMock.Setup(x => x.FindLesson(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(lesson);
        _mapperMock.Setup(x => x.Map<LessonModel>(It.IsAny<Lesson>()))
            .Returns(new LessonModel(
                lesson.Id,
                lesson.Name,
                lesson.Description,
                lesson.DateFrom,
                lesson.Duration,
                lesson.MaxStudents,
                new LessonTypeModel((int)lesson.LessonType, lesson.LessonType.ToString()),
                new TrainingLevelModel((int)lesson.TrainingLevel, lesson.TrainingLevel.ToString()), 
                new LessonStatusModel((int)lesson.LessonStatus, lesson.LessonStatus.ToString()), 
                new TrainerModel(Guid.NewGuid(), "Trainer Name"),
                new List<StudentModel>())
            );

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        _contextMock.Verify(x => x.LessonGroups.RemoveRange(It.IsAny<IEnumerable<LessonGroup>>()), Times.Once);
        _contextMock.Verify(x => x.Lessons.Remove(It.IsAny<Lesson>()), Times.Once);
        _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _lessonServiceAppMock.Verify(x => x.FindLesson(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

        Assert.Equal("Lesson has been deleted.", result.Message);
    }

    [Fact]
    public async Task Handle_LessonNotFound_Should_Throw_Exception()
    {
        // Arrange
        var lessonId = Guid.NewGuid();
        var command = new DeleteLessonCommand(lessonId);
        var cancellationToken = new CancellationToken();

        _lessonServiceAppMock.Setup(x => x.FindLesson(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Lesson not found."));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, cancellationToken));

        _contextMock.Verify(x => x.Lessons.Remove(It.IsAny<Lesson>()), Times.Never);
        _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Exception_Should_Log_Error_And_Throw()
    {
        // Arrange
        var lessonId = Guid.NewGuid();
        var lesson = new Lesson { Id = lessonId };
        var command = new DeleteLessonCommand(lessonId);
        var cancellationToken = new CancellationToken();

        _lessonServiceAppMock.Setup(x => x.FindLesson(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(lesson);
        _contextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error."));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, cancellationToken));

        _loggerMock.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
    }
}
