﻿using EmployeeService.Controllers;
using EmployeeService.Models;
using Microsoft.AspNetCore.Mvc;
using NLog.Web.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_app_hw.Controllers;
using web_app_hw.Models;
using Xunit.Priority;

namespace EmployeeServiceTests
{
    public class SampleTests
    {
        private SampleController _sampleController;
        private SampleObjectPool _pool;

        public SampleTests()//класс создается по количеству тестов 
        {
            _pool = SampleObjectPool.Instance;
            _sampleController = new SampleController(_pool);
        }

        [Theory, Priority(1)]
        [InlineData(5)]
        [InlineData(15)]
        [InlineData(20)]
        public void CreateSampleObjectTest(int id)
        {
            ActionResult<bool> result = _sampleController.Create(id);
            Assert.IsAssignableFrom<ActionResult<bool>>(result);
        }

        [Fact, Priority(2)]
        public void GetAllSampleObjectTest()
        {
            var response = _sampleController.GetAll();
            Assert.NotNull(response.Result);//то что вернут не должно быть равно нулю
            Assert.IsAssignableFrom<OkObjectResult>(response.Result);
            Assert.IsAssignableFrom<List<SampleObject>>(((OkObjectResult)response.Result).Value);
            Assert.NotEmpty((List<SampleObject>)((OkObjectResult)response.Result).Value);//что вернут не должно быть пустым
        }


    }
}
