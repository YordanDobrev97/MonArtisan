﻿namespace MonArtisan.Services.Data
{
    using System.Threading.Tasks;

    public interface IProjectsService
    {
        Task<bool> Create(string name);
    }
}
