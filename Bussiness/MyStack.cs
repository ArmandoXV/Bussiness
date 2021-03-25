using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
  public  class MyStack<T>
    {
        private readonly List<T> _list = new List<T>();
        public bool IsEmpty => Count == 0;
        public int Count => _list.Count;
        public void Push(T value) {
            _list.Add(value);

        }

        public void Pop() {
            if (IsEmpty)
                throw new InvalidOperationException();
            _list.RemoveAt(Count - 1);
        }

        public T Peek() {
            return _list[Count - 1];
        }
    }
}
