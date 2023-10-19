﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IList<Service>> ListAsync()
        {
            var result = await _context.Services
                .Where(x => x.Enabled == true)

                .ToListAsync();

            return result;
        }

        public async Task<Service> FindByIdAsync(int id)
        {
            var result = await _context.Services
                .Where(x => x.Id == id)
                .Where(x => x.Enabled == true)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Service> AddServiceCarAsync(Service service)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var cars = service.Cars;

                    var foundedCar = await _context.Car.FindAsync(cars.FirstOrDefault().Id);

                    service.Cars = new List<Car>();

                    service.Cars.Add(foundedCar);

                    _context.Services.Add(service);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return service;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}