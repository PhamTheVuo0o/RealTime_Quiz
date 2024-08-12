using AutoMapper;
using AppCore.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Identity.API.Jobs.Hangfire
{
    public class HangfireJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private DataContext _context;
        public HangfireJob(DataContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
