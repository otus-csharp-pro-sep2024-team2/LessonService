﻿using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using MediatR;

namespace LessonService.Commands.Commands.Request;

public record EnrollStudentCommand(Guid LessonId, Guid StudentId, string StudentName):  IRequest<ApiResponse<LessonModel>>;
