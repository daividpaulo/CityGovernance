using CityGovernance.Domain.Exceptions;
using CityGovernance.Domain.Interfaces;
using CityGovernance.Domain.Models;
using CityGovernance.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CityGovernance.Tests.UnitTests
{

    public class CityServiceTest
    {

        private CityService service;
        private readonly Mock<ICityRepository> _cityRepository;

        public CityServiceTest()
        {
            _cityRepository = new Mock<ICityRepository>();
            service = new CityService(_cityRepository.Object);
        }

        [Fact]
        public void UpdateCity_Deve_Lancar_excecao_quando_Ibge_CidadeUF_Existente()
        {
            _cityRepository.Setup(x => x.IsValid(new City())).Returns(false);

            var exception = Assert.Throws<ExistCityException>(() => service.UpdateCity(1, new City()));

            Assert.Equal("Já existe uma cidade cadastrada para uma das opções informadas [ Nome ou Ibge ou Uf ]", exception.Message);
        }

        

    }
}
