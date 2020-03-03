using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanFunc.Application.Common.Interfaces;
using CleanFunc.Application.Issuers.Queries.GetIssuer;
using CleanFunc.Application.UnitTests.Common;
using Shouldly;
using Xunit;

namespace CleanFunc.Application.UnitTests.Issuers.Queries.GetIssuer
{
    [Collection("QueryTests")]
    public class GetIssuerQueryTests
    {
        private readonly IIssuerRepository _context;
        private readonly IMapper _mapper;
        
        public GetIssuerQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectResponseAndListCount()
        {
            var query = new GetIssuerQuery();
            query.Id = new Guid("5f95d690-513a-497f-bba2-76bc286bf2af");

            var handler = new GetIssuerQuery.GetIssuerQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<GetIssuerResponse>();
            
            result.Issuer.ShouldNotBeNull();
            // result.Lists.Count.ShouldBe(1);

            // var list = result.Lists.First();

            // list.Items.Count.ShouldBe(5);
        }
    }
}