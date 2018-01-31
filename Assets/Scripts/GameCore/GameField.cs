using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunayTriangulator;

public class Transmition
{
    public float increase_value;
    public float increase_timer;
}

public class Station : Vertex {

    public List<Station> Neighbours;
    public int Owner;
    public float Radiation;
    private List<Transmition> _transmitions;


    public Station(float x, float y) : base(x,y)
    {
        Neighbours = new List<Station>();
        _transmitions = new List<Transmition>();
    }

    // add fake news to this point
    public void Increase(int player, float intensity, float timer)
    {
        Transmition transmition = new Transmition();
        transmition.increase_timer = timer;
        transmition.increase_value = intensity;
        _transmitions.Add(transmition);
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

    public void Update(float dt, TransmitionEmitterPool emittersPool, SpreadNewsPerson[] spammedPersons)
    {
        Decrease(GameBalanceConst.GlobalCensorAbsorption * dt);

        List<Transmition> transmitions_to_delete = new List<Transmition>();
        foreach (var transmition in _transmitions)
        {
            if (transmition.increase_timer >= 0.0f)
            {
                transmition.increase_timer -= dt;
                if (transmition.increase_timer <= 0.0f)
                {                   
                    Radiation += transmition.increase_value;

                    if (Radiation > GameBalanceConst.MaxIntensityToDropDown)
                    {
                        Radiation = 0;
                        SpreadNewsPerson person = GameObject.Instantiate(spammedPersons[Random.Range(0, spammedPersons.Length)]);
                        person.ShowSpreadingNews(GetPosition());
                        GameObject.Destroy(person, 2.0f);
                    }
                    else
                    {
                        if (transmition.increase_value > 0 && emittersPool.transform.childCount < GameBalanceConst.MaximumTransitions)
                        {
                            int curWidth = GameBalanceConst.SpreadSize;
                            foreach (var nearPoint in Neighbours)
                            {
                                float timeToflight = GameBalanceConst.Asynchronous ? Random.Range(0.8f * GameBalanceConst.TransmissionsTime, 1.75f * GameBalanceConst.TransmissionsTime) : GameBalanceConst.TransmissionsTime;
                                GameObject emitter = emittersPool.InstantiateEmitter();
                                emitter.transform.position = GetPosition();
                                LeanTween.move(emitter, nearPoint.GetPosition(),timeToflight);
                                GameObject.Destroy(emitter, timeToflight + 0.01f);
                                nearPoint.Increase(1, transmition.increase_value - GameBalanceConst.RadiationAbsorption, timeToflight);
                                curWidth -= 1;
                                if (curWidth == 0) break;
                            }
                        }
                    }
                    transmitions_to_delete.Add(transmition);
                }
            }
        }

        foreach(var deleteTransmition in transmitions_to_delete)
        {
            _transmitions.Remove(deleteTransmition);
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

    public float Update(float dt, TransmitionEmitterPool emittersPool, SpreadNewsPerson[] spammedPersons)
    {
        float percentComplete = 0.0f;
        float radiatedPoints = 0;

        foreach(var point in Points)
        {           
            if (point.Radiation > 0.0f)
            {
                radiatedPoints += 1.0f;
            }
            point.Update(dt, emittersPool, spammedPersons);
        }

        percentComplete = (radiatedPoints / (float)Points.Count) ;

        return percentComplete;
    }
}
