using AutoMapper;
using LessonService.Application.Models.Lesson;
using LessonService.Application.Services;
using LessonService.Application.Services.Mapping;
using LessonService.Domain.Entities;
using LessonService.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace LessonService.Tests;

public class LessonServiceAppTests
{
    private readonly LessonServiceApp _lessonServiceApp;
    private readonly AppDbContext _context;
    private readonly string _unitTestData = "UNIT TEST DATA";
    
    public LessonServiceAppTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "LessonServiceTestDb")
            .Options;
        _context = new AppDbContext(options);
        Mock<ILogger<LessonServiceApp>> loggerMock = new();
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new LessonMapping()));
        var mapper = config.CreateMapper();
        _lessonServiceApp = new LessonServiceApp(_context, loggerMock.Object, mapper);
    }

    [Fact]
    public async Task GetAllLessonsAsync_ReturnsAllLessons()
    {
        var resultBefore = await _lessonServiceApp.GetAllLessonsAsync();
        // Arrange
        var lessonOne = new Lesson("ReturnsAllLessons 1", _unitTestData);
        _context.Lessons.Add(lessonOne);
        var lessonTwo = new Lesson("ReturnsAllLessons 2", _unitTestData);
        _context.Lessons.Add(lessonTwo);
        await _context.SaveChangesAsync();

        // Act
        var resultAfter = await _lessonServiceApp.GetAllLessonsAsync();

        // Assert
        Assert.NotNull(await _context.Lessons.FindAsync(lessonOne.Id));
        Assert.NotNull(await _context.Lessons.FindAsync(lessonTwo.Id));
        Assert.Equal(resultBefore.Count()+2, resultAfter.Count());
    }
/*
    [Fact]
    public async Task CreateLessonAsync_CreatesLesson()
    {
        // Arrange
        var request = new CreateLessonRequest("Lesson CreateLessonAsync", _unitTestData, DateTime.UtcNow, 60, 10, 1, 1,
            Guid.NewGuid(), "Trainer Name 1");

        // Act
        var result = await _lessonServiceApp.CreateLessonAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.Name, result.Name);
    }
*/
    [Fact]
    public async Task GetLessonByIdAsync_ReturnsLesson()
    {
        // Arrange
        var lesson = await NewLessonAsync("Lesson GetLessonByIdAsync");
        
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        // Act
        var result = await _lessonServiceApp.GetLessonByIdAsync(lesson.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(lesson.Name, result.Name);
    }

    [Fact]
    public async Task DeleteLessonAsync_DeletesLesson()
    {
        // Arrange
        var lesson = await NewLessonAsync("Lesson 1");
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        // Act
        var result = await _lessonServiceApp.DeleteLessonAsync(lesson.Id);

        // Assert
        Assert.True(result);
        Assert.Null(await _context.Lessons.FindAsync(lesson.Id));
    }
   
    private Task<Lesson> NewLessonAsync(string name)
    {
        var lesson = new Lesson ( name, _unitTestData );
        return Task<Lesson>.Factory.StartNew(() => lesson);
    }
}