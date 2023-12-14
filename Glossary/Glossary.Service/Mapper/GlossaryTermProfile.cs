using AutoMapper;
using Glossary.Service.Common.Models;
using Glossary.Service.Domains;

namespace Glossary.Service.Mapper;

public class GlossaryTermProfile : Profile
{
    public GlossaryTermProfile()
    {
        CreateMap<GlossaryTerm, GlossaryTermDto>();
        CreateMap<List<GlossaryTerm>, GlossaryTermDto>();
    }
}