using System.Collections.Generic;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Chapter3
{
    /// <summary>
    /// An animal shelter holds only dogs and cats, and operates on a strictly "first in,
    /// first out" basis. People must adopt either the "oldest" (based on arrival time) of
    /// all animals at the shelter, or they can select whether they would prefer a dog or
    /// a cat (and will receive the oldest animal of that type). They cannot select which
    /// specific animal they would like. Create the data structures to maintain this system
    /// and implement operations such as enqueue, dequeueAny, dequeueDog and
    /// dequeueCat. You may use the built-in LinkedList data structure.
    /// </summary>
    public class Q3P7AnimalShelter
    {
        public class AnimalShelter
        {
            private readonly LinkedList<Animal> _animals = new LinkedList<Animal>();

            public void Enqueue(Animal a)
            {
                if (_animals.Count == 0)
                {
                    _animals.AddFirst(a);
                }
                else
                {
                    var current = _animals.First;
                    while (current != null)
                    {
                        if (a.Age > current.Value.Age)
                        {
                            _animals.AddBefore(current, a);
                            return;
                        }

                        current = current.Next;
                    }

                    _animals.AddLast(a);
                }
            }

            public Animal DequeueAny()
            {
                var animal = _animals.First.Value;
                _animals.RemoveFirst();
                return animal;
            }

            private Animal Dequeue<T>()
            {
                var current = _animals.First;

                while (current != null)
                {
                    if (current.Value is T)
                    {
                        var animal = current.Value;
                        _animals.Remove(current);
                        return animal;
                    }

                    current = current.Next;
                }

                return null;
            }

            public Animal DequeueDog()
            {
                return Dequeue<Dog>();
            }

            public Animal DequeueCat()
            {
                return Dequeue<Cat>();
            }
        }

        public abstract class Animal
        {
            public int Age { get; private set; }

            protected Animal(int age)
            {
                Age = age;
            }
        }

        public class Cat : Animal
        {
            public Cat(int age) : base(age)
            {
            }
        }

        public class Dog : Animal
        {
            public Dog(int age) : base(age)
            {
            }
        }

        [TestFixture]
        public class Q3P7AnimalShelterTests
        {
            [Test]
            public void AnimalShelterTest()
            {
                var shelter = new AnimalShelter();
                shelter.Enqueue(new Dog(4));
                shelter.Enqueue(new Cat(3));
                shelter.Enqueue(new Cat(6));
                shelter.Enqueue(new Cat(5));

                Assert.AreEqual(6, shelter.DequeueAny().Age);
                Assert.AreEqual(5, shelter.DequeueAny().Age);
                Assert.AreEqual(4, shelter.DequeueAny().Age);
                Assert.AreEqual(3, shelter.DequeueAny().Age);
            }

            [Test]
            public void AnimalShelterTest2()
            {
                var shelter = new AnimalShelter();
                shelter.Enqueue(new Dog(4));
                shelter.Enqueue(new Cat(3));
                shelter.Enqueue(new Cat(6));
                shelter.Enqueue(new Cat(5));
                shelter.Enqueue(new Dog(5));
                shelter.Enqueue(new Cat(8));

                Assert.AreEqual(5, shelter.DequeueDog().Age);
                Assert.AreEqual(4, shelter.DequeueDog().Age);

                shelter.Enqueue(new Dog(8));
                shelter.Enqueue(new Dog(9));
                shelter.Enqueue(new Cat(5));

                Assert.AreEqual(8, shelter.DequeueCat().Age);
                Assert.AreEqual(6, shelter.DequeueCat().Age);
            }
        }
    }
}