using SuMvc.Core.Data;
using SuMvc.Core.Domain.Tests;


namespace SuMvc.Services.Test
{
    public class TestService:ITestService
    {
        private readonly IRepository<TestEtity> _testRepository;

        public TestService(
            IRepository<TestEtity> testRepository)
        {
            _testRepository = testRepository;
        }

        public void InsertTest(TestEtity entity)
        {
            _testRepository.Insert(entity);
        }
    }
}