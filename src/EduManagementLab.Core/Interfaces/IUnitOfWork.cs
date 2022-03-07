﻿using EduManagementLab.Core.Interfaces.Repositories;


namespace EduManagementLab.Core.Interfaces
{
    public interface IUnitOfWork
    {

        IUserRepository Users { get; }
        ICourseRepository Courses { get; }
        ICourseLineItemRepository CourseLineItems { get; }
        int Complete();
    }
}
