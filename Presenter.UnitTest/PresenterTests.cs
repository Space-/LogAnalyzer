using System;
using NSubstitute;
using NUnit.Framework;

namespace PresenterTests.UnitTest
{
    public class PresenterTests
    {
        // p.126~127
        [Test]
        public void Ctor_WhenViewIsLoaded_CallsViewRender()
        {
            var mockView = Substitute.For<IView>();
            var presenter = new Presenter(mockView);

            mockView.Loaded += Raise.Event<Action>();
            mockView.Received().Render(Arg.Is<string>(x => x.Contains("Hello world")));
        }
    }

    public class Presenter
    {
        private readonly IView _view;

        public Presenter(IView view)
        {
            _view = view;
            _view.Loaded += OnLoaded;
        }

        public void OnLoaded()
        {
            _view.Render("Hello world");
        }
    }

    public interface IView
    {
        event Action Loaded;

        void Render(string text);
    }
}