using AutoMapper;
using TestTaskGFL.Bll.DTOs;
using TestTaskGFL.Models;

namespace TestTaskGFL.Bll.Profiles
{
    //Profiler to map folders
    public class FolderProfiler : Profile
    {
        public FolderProfiler()
        {
            CreateMap<Folder, FolderDTO>();
        }
    }
}
