using SuMvc.Core.Domain.Tests;

namespace SuMvc.Services.Test
{
    public interface ITestService:IService
    {
        void InsertTest(TestEtity entity);
    }
}