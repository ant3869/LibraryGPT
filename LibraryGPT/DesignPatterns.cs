using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    internal class DesignPatterns
    {

    }

    // Creational Patterns
    public sealed class Singleton
    {
        private static readonly Singleton instance = new Singleton();

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }
    }

    // Factory Method
    public interface IProduct
    {
        string GetName();
    }

    public class ConcreteProductA : IProduct
    {
        public string GetName() => "Product A";
    }

    public abstract class Creator
    {
        public abstract IProduct FactoryMethod();
    }

    public class ConcreteCreatorA : Creator
    {
        public override IProduct FactoryMethod() => new ConcreteProductA();
    }

    //  Adapter
    public interface ITarget
    {
        void Request();
    }

    public class Adaptee
    {
        public void SpecificRequest() { }
    }

    public class Adapter : ITarget
    {
        private readonly Adaptee _adaptee = new Adaptee();

        public void Request()
        {
            _adaptee.SpecificRequest();
        }
    }

    // Decorator
    public abstract class Component
    {
        public abstract void Operation();
    }

    public class ConcreteComponent : Component
    {
        public override void Operation() { }
    }

    public abstract class Decorator : Component
    {
        protected Component component;

        public void SetComponent(Component component)
        {
            this.component = component;
        }

        public override void Operation()
        {
            component?.Operation();
        }
    }

    // Observer

    public interface IObserver
    {
        void Update();
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    public class ConcreteSubject : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }

    // Strategy
    public interface IStrategy
    {
        void AlgorithmInterface();
    }

    public class ConcreteStrategyA : IStrategy
    {
        public void AlgorithmInterface() { }
    }

    public class Context
    {
        private IStrategy _strategy;

        public Context(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void ContextInterface()
        {
            _strategy.AlgorithmInterface();
        }
    }
}
