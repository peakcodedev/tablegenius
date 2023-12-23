using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Course;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class CoursePresenter : BasePresenter<CourseRM, Course>,
    ICoursePresenter
{
    private readonly ICourseService _courseService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public CoursePresenter(IMapper mapper,
        ICourseService courseService, IUserService userService) : base(
        courseService, mapper)
    {
        _courseService = courseService;
        _mapper = mapper;
        _userService = userService;
    }

    public CourseRM Add(CourseRM entity)
    {
        var model = _mapper.Map<Course>(entity);
        var result = _courseService.Add(model);
        return _mapper.Map<CourseRM>(result);
    }

    public CourseRM GetByUserId(Guid userId)
    {
        var result = _courseService.GetByUserId(userId);
        return _mapper.Map<CourseRM>(result);
    }

    public override CourseRM GetBlank()
    {
        return new CourseRM();
    }

    public override void UpdateBlank(CourseRM entity)
    {
        // NOTHING TO DO HERE
    }

    public IEnumerable<ExportCourseRM> GetExportList()
    {
        var all = _courseService.GetAllAsNoTracking().ToList();
        var exportCourses = _mapper.Map<IEnumerable<Course>, List<ExportCourseRM>>(all);
        foreach (var course in exportCourses)
        {
            var user = _userService.GetByIdAsNoTracking(course.UserId);
            course.FirstName = user.FirstName;
            course.LastName = user.LastName;
            course.Street = user.Address;
            course.Zip = user.ZipCode;
            course.City = user.City;
            course.Mail = user.Mail;
        }

        return exportCourses;
    }
}