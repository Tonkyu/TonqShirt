using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using BModel = Unity.Barracuda.Model;

public class Worker : MonoBehaviour
{
    [SerializeField] private NNModel model;
    private IWorker worker;

    private void Start()
    {
        // 利用する際はrun-time model(Model型)に変換する
        BModel runtimeModel = ModelLoader.Load(model);

        // Workerを作成する
        worker = WorkerFactory.CreateWorker(runtimeModel); 

        // GPU
        // worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, runtimeModel);
        // worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Compute, runtimeModel);
        // slow - GPU path
        // worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputeRef, runtimeModel);

        // CPU
        // worker = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharpBurst, runtimeModel);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharp, runtimeModel);
        // very slow - CPU path
        // worker = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharpRef, runtimeModel);
    }

    void Update()
    {

    }

    public Controller.ControlMode Infer(Quaternion q)
    {
        Tensor input = new Tensor(n: 1, h: 1, w: 1, c: 4);
        input[0,0,0,0] = q.w;
        input[0,0,0,1] = q.x;
        input[0,0,0,2] = q.y;
        input[0,0,0,3] = q.z;
        worker.Execute(input);

        Tensor output = worker.PeekOutput();
        float tmp = -100;
        int ans = 0;
        for (var i = 0; i < output.channels; i++)
        {
            if (tmp < output[0,0,0,i])
            {
                tmp = output[0,0,0,i];
                ans = i;
            }
        }
        input.Dispose();
        return (Controller.ControlMode) ans;
    }
}
