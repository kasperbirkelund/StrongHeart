using System.Text;

namespace StrongHeart.Features.Test.Decorators.Authorization.Features.Queries.TestQuery
{
    public class PersonDto
    {
        public string Name { get; }

        public PersonDto(string name)
        {
            Name = name;
        }
    }
}
