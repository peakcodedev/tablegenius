using AutoMapper;
using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Entities.Course;
using TableGenius.Api.Entities.General;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Mapper;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<User, UserRM>();
        CreateMap<UserRM, User>();

        CreateMap<Project, ProjectRm>();
        CreateMap<ProjectRm, Project>();

        CreateMap<ProjectRating, ProjectRatingRm>();
        CreateMap<ProjectRatingRm, ProjectRating>();

        CreateMap<ProjectUserRating, ProjectUserRatingRM>();
        CreateMap<ProjectUserRatingRM, ProjectUserRating>();

        CreateMap<ProjectExpert, ProjectExpertRm>();
        CreateMap<ProjectExpertRm, ProjectExpert>();

        CreateMap<ProjectBooth, ProjectBoothRm>();
        CreateMap<ProjectBooth, ExportProjectBoothRm>();
        CreateMap<ProjectBoothRm, ProjectBooth>();

        CreateMap<Company, CompanyRm>();
        CreateMap<CompanyRm, Company>();

        CreateMap<Employee, EmployeeRm>();
        CreateMap<EmployeeRm, Employee>();

        CreateMap<Profession, ProfessionRm>();
        CreateMap<ProfessionRm, Profession>();

        CreateMap<EmployeeModel, Employee>();
        CreateMap<ProjectCollaborationModel, ProjectCollaboration>();

        CreateMap<Employee, EmployeeRm>();
        CreateMap<ProjectCollaboration, ProjectCollaborationRm>();

        CreateMap<UserRM, ExportUserRM>();
        CreateMap<ProjectImage, ProjectImageRm>();

        CreateMap<Course, CourseRM>();
        CreateMap<Course, ExportCourseRM>();
        CreateMap<CourseRM, Course>();
    }
}