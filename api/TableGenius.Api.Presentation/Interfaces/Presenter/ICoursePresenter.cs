using System;
using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface ICoursePresenter : IPresenter<CourseRM>
{
    CourseRM Add(CourseRM entity);
    CourseRM GetByUserId(Guid userId);
    IEnumerable<ExportCourseRM> GetExportList();
}