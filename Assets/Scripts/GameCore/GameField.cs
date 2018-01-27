using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunayTriangulator;

public class Station : Vertex {

    public List<Station> Neighbours;
    public int Owner;
    public float Radiation;

    public float increase_value;
    public float increase_timer;

    public Station(float x, float y) : base(x,y)
    {
        Neighbours = new List<Station>();
        increase_timer = -1.0f;
        increase_value = 0.0f;
    }

    // add fake news to this point
    public void Increase(int player, float intensity, float timer)
    {
        increase_timer = timer;
        increase_value = intensity;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(x, y, 0.0f);
    }

    public void AddNeighbour(Station point)
    {
        if (!Neighbours.Contains(point))
        {
            Neighbours.Add(point);
        }
    }

    public void Decrease(float intensity )
    {
        Radiation = Mathf.Max(0.0f, Radiation - intensity);
    }

    public void Kill()
    {
        Radiation = 0;
        Owner = 0;
    }

    public void Update(float dt)
    {
        Decrease(GameBalanceConst.GlobalCensorAbsorption * dt);

        if (increase_timer>=0.0f)
        {
            increase_timer -= dt;
            if (increase_timer <= 0.0f)
            {
                increase_timer = -1;
                Radiation += increase_value;
                increase_value -= GameBalanceConst.RadiationAbsorption;
                if (increase_value > 0)
                {
                    int curWidth = GameBalanceConst.SpreadSize;
                    foreach (var nearPoint in Neighbours)
                    {
                        nearPoint.Increase(1, increase_value, 0.1f);
                        curWidth -= 1;
                        if (curWidth == 0) break;
                    }
                }
            }
        }

    }

}

public class GameField {

    public List<Station> Points;
    public Vector3 Origin;
    public Vector3 Size;

    private Triangulator _triangulator;

    public void Init(Vector3 size)
    {
        Points = new List<Station>();
        //Origin = origin;
        Size = size;
        _triangulator = new Triangulator();
    }

    public void GenerateField(int pointsCount)
    {
        Points.Clear();
        List<Vertex> vertexes = new List<Vertex>();
        for (int iCurPoint = 0; iCurPoint < pointsCount; ++iCurPoint)
        {
            Vector3 newPos = new Vector3(Random.Range(0, Size.x), Random.Range(0, Size.y));
            Station newPoint = new Station(newPos.x, newPos.y);
            Points.Add(newPoint);
            vertexes.Add(newPoint);
        }
        List<Triad> triads = _triangulator.Triangulation(vertexes);
        foreach(var triangle in triads)
        {
            Station a = Points[triangle.a];
            Station b = Points[triangle.b];
            Station c = Points[triangle.c];

            a.AddNeighbour(b);
            a.AddNeighbour(c);

            b.AddNeighbour(a);
            b.AddNeighbour(c);

            c.AddNeighbour(a);
            c.AddNeighbour(b);
        }
    }

    public void Update(float dt)
    {
        foreach(var point in Points)
        {           
            point.Update(dt);
        }
    }
}
