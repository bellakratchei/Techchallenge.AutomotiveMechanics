﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Contract;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IBaseNotification _baseNotification;

        public CarService(ICarRepository carRepository, IMapper mapper, IBaseNotification baseNotification)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _baseNotification = baseNotification;
        }

        public async Task<IList<CarResult>> ListAsync()
        {
            var result = await _carRepository.ListAsync();

            return _mapper.Map<IList<CarResult>>(result);
        }

        public async Task<CarResult> FindByIdAsync(int id)
        {
            var result = await _carRepository.GetByIdAsync(id);

            return _mapper.Map<CarResult>(result);
        }

        public async Task<CarResult> AddAsync(CarInsertInput input)
        {
            var contract = new CarContract(input);

            if (!contract.IsValid)
            {
                _baseNotification.AddNotifications(contract.Notifications);
                return default;
            }

            var map = _mapper.Map<Car>(input);

            var result = new CarResult();

            var inserted = await _carRepository.AddAsync(map);

            if (inserted > 0)
            {
                result = _mapper.Map<CarResult>(map);
            }

            return result;
        }

        public async Task<CarResult> UpdateAsync(CarUpdateInput input)
        {
            var map = _mapper.Map<Car>(input);

            var founded = await _carRepository.GetByIdAsync(map.Id);

            founded.LastModifiedDate = DateTime.Now;
            founded.Enabled = true;

            var result = new CarResult();

            var updated = await _carRepository.UpdateAsync(founded);

            if (updated > 0)
            {
                result = _mapper.Map<CarResult>(founded);
            }

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var founded = await _carRepository.GetByIdAsync(id);

            founded.LastModifiedDate = DateTime.Now;
            founded.Enabled = false;

            return await _carRepository.UpdateAsync(founded) > 0;
        }

    }
}