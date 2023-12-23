namespace TableGenius.Api.Presentation.Interfaces.Mapper;

public interface ICustomMapper<out T, in U> where T : class where U : class
{
    T Map(U input);
}