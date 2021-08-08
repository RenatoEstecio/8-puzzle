using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA8p.Class
{
    public  class Pilha
    {
        public List<Matriz> Stack;
        public int Topo;
        public Pilha()
        {
            Topo = 0;
            Stack = new List<Matriz>();
        }
        public void push(Matriz y)
        {
            Stack.Add(y);
            Topo++;
        }
        public Matriz pop()
        {
            Topo--;
            Matriz m = Stack[Topo];
            Stack.RemoveAt(Topo);
            return m;
        }
        public Matriz top()
        {                    
            return Stack[Topo];
        }
    }
}
