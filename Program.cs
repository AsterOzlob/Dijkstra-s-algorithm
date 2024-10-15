using System;

//Класс, описывающий граф
class Graph
{
    private int[,] adjancencyMatrix;

    public Graph(int size)
    {
        adjancencyMatrix = new int[size, size];
    }

    //Функция добавления узла графа
    //Аргументы: индекс вершины из которой исходит ребро,
    //индекс вершины, в которую входит ребро,
    //стоимость прохождения от вершины к вершине
    public void AddEdge(int source, int destination, int weight)
    {
        adjancencyMatrix[source, destination] = weight;
    }

    //Метод для получения матрицы смежности
    //Возвращает: матрицу смежности
    public int[,] GetAdjacencyMatrix()
    {
        return adjancencyMatrix;
    }
}

//Класс, реализующий метод Дейкстры
class Dijkstra
{
    //Функция поиска наикратчайшего пути методом Дейкстры
    //Аргументы: граф, начальная вершина, конечная вершина
    public static void ShortestPath(Graph graph, int source, int destination)
    {
        int size = graph.GetAdjacencyMatrix().GetLength(0);
        int[] distance = new int[size];
        int[] previous = new int[size];
        bool[] visited = new bool[size];

        // Инициализация расстояний и предыдущих вершин
        for (int i = 0; i < size; i++)
        {
            distance[i] = int.MaxValue;
            previous[i] = -1;
        }
        distance[source] = 0;

        //Поиск вершины с минимальным весом из ещё непосещённых
        for (int i = 0; i < size - 1; i++)
        {
            int minDistance = int.MaxValue;
            int minIndex = -1;

            for (int j = 0; j < size; j++)
            {
                if (!visited[j] && distance[j] < minDistance)
                {
                    minDistance = distance[j];
                    minIndex = j;
                }
            }

            visited[minIndex] = true;

            for (int j = 0; j < size; j++)
            {
                if (!visited[j] && graph.GetAdjacencyMatrix()[minIndex, j] != 0
                    && distance[minIndex] != int.MaxValue
                    && distance[minIndex] + graph.GetAdjacencyMatrix()[minIndex, j] < distance[j])
                {
                    distance[j] = distance[minIndex] + graph.GetAdjacencyMatrix()[minIndex, j];
                    previous[j] = minIndex;
                }
            }
        }

        //Вывод кратчайшего пути и его топологии
        List<int> path = new();
        int current = destination;

        while(current != - 1)
        {
            path.Insert(0, current);
            current = previous[current];
        }

        Console.Write("Топология кратчайшего пути: ");
        foreach (int vertex in path)
        {
            Console.Write(vertex + 1 + " ");
        }

        int shortestDistance = distance[destination];
        Console.WriteLine("\nВес кратчайшего пути: " + shortestDistance);
    }
}

class Program
{ 
    static void Main()
    {
        //Заголовок программы
        Console.WriteLine("Программа, реализующая метод Дейкстры\n");

        Graph graph = new(8);

        //Занесение узлов графа
        graph.AddEdge(0, 1, 560);
        graph.AddEdge(0, 2, 670);
        graph.AddEdge(0, 3, 920);
        graph.AddEdge(1, 3, 440);
        graph.AddEdge(1, 4, 730);
        graph.AddEdge(2, 3, 480);
        graph.AddEdge(2, 5, 440);
        graph.AddEdge(3, 4, 390);
        graph.AddEdge(3, 5, 520);
        graph.AddEdge(4, 6, 380);
        graph.AddEdge(4, 7, 670);
        graph.AddEdge(5, 6, 580);
        graph.AddEdge(5, 7, 620);
        graph.AddEdge(6, 7, 390);

        Dijkstra.ShortestPath(graph, 0, 7);
    }
}


/*
graph.AddEdge(0, 1, 2.6); //1->2
graph.AddEdge(0, 2, 1.8); //1->3
graph.AddEdge(0, 3, 3.2); //1->4
graph.AddEdge(1, 3, 5.6); //2->4
graph.AddEdge(1, 4, 7.6); //2->5
graph.AddEdge(2, 3, 2.1); //3->4
graph.AddEdge(2, 5, 5.2); //3->6
graph.AddEdge(3, 4, 1.6); //3->5
graph.AddEdge(3, 6, 8.9); //3->7
graph.AddEdge(4, 7, 12.1); //5->8
graph.AddEdge(5, 8, 4.2); //6->9
graph.AddEdge(6, 4, 7.1); //7->5
graph.AddEdge(6, 5, 2.9); //7->6
graph.AddEdge(6, 7, 3.6); //7->8
graph.AddEdge(6, 9, 9.2); //7->10
graph.AddEdge(7, 9, 11.5); //8->10
graph.AddEdge(8, 6, 7.5); //9->7
graph.AddEdge(8, 9, 8.7); //9->10
*/