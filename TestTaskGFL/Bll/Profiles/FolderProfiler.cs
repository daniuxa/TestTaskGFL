using AutoMapper;
using TestTaskGFL.Bll.DTOs;
using TestTaskGFL.Models;

namespace TestTaskGFL.Bll.Profiles
{
    public class FolderProfiler : Profile
    {
        public FolderProfiler()
        {
            CreateMap<Folder, FolderDTO>();
        }
    }
}
