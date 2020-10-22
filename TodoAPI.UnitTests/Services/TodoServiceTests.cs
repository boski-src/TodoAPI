using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TodoAPI.Contexts.Todos;
using TodoAPI.Contexts.Todos.Repositories;

namespace TodoAPI.UnitTests.Services
{
    [TestFixture]
    public class TodoServiceTests
    {
        private List<Todo> _todos;
        private ITodoRepository _todoRepository;

        [SetUp]
        public void SetUp()
        {
            _todos = new List<Todo> {
                new Todo(Guid.NewGuid(), "test1Title", "test1Desc", DateTime.UtcNow.AddHours(5)),
                new Todo(Guid.NewGuid(), "test2Title", "test2Desc", DateTime.UtcNow.AddDays(1)),
                new Todo(Guid.Empty, "test3Title", "test3Desc", DateTime.UtcNow.AddDays(10))
            };

            var todoRepositoryMock = new Mock<ITodoRepository>();

            todoRepositoryMock.Setup(x => x.Browse())
                              .Returns(Task.FromResult(_todos));
            todoRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>()))
                              .Returns(Task.FromResult(_todos.FirstOrDefault()));

            todoRepositoryMock.Setup(x => x.Create(It.IsAny<Todo>()))
                              .Returns<Todo>(
                                  t => {
                                      _todos.Add(t);
                                      return Task.FromResult(t.Id);
                                  }
                              );

            _todoRepository = todoRepositoryMock.Object;
        }

        [Test]
        public async Task Browse_Test()
        {
            var todos = await _todoRepository.Browse();

            Assert.AreEqual(3, todos.Count);
        }

        [Test]
        public async Task Get_Test()
        {
            var todo = await _todoRepository.Get(Guid.Empty);
            Assert.NotNull(todo);
        }

        [Test]
        public async Task Create_Test()
        {
            await _todoRepository.Create(
                new Todo(
                    Guid.NewGuid(), 
                    "testCreate", 
                    "Desc", 
                    DateTime.Now.AddDays(2))
            );

            Assert.Pass();
        }
    }
}