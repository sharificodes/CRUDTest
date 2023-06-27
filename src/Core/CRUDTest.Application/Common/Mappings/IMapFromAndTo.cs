using AutoMapper;
using System.Reflection;

namespace CRUDTest.Application.Common.Mappings;

public interface IMapFromAndTo<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
}