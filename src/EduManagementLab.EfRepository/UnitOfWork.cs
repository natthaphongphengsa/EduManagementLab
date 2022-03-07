﻿using EduManagementLab.Core.Interfaces;
using EduManagementLab.Core.Interfaces.Repositories;
using EduManagementLab.EfRepository.Repositories;

namespace EduManagementLab.EfRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;

            Users = new UserRepository(_context);
            Courses = new CourseRepository(_context);
            CourseLineItems = new CourseLineItemRepository(_context);

        }

        public IUserRepository Users { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public ICourseLineItemRepository CourseLineItems { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
