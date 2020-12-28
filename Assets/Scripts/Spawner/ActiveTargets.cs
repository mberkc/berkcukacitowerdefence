using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTargets : MonoBehaviour, IActiveTargetsAction {
    public event IActiveTargetsAction.QueueAction OnQueueUpdate;

    [SerializeField] Queue<Monster> monsterQueue = new Queue<Monster>();

    public void ResetQueue() {
        monsterQueue.Clear();
        OnQueueUpdate?.Invoke(monsterQueue);
    }

    public void AddTarget(Monster monster) {
        monsterQueue.Enqueue(monster);
        OnQueueUpdate?.Invoke(monsterQueue);
    }

    public void RemoveTarget(Monster monster) {
        if (monsterQueue.Contains(monster))
            monsterQueue.Dequeue();
        OnQueueUpdate?.Invoke(monsterQueue);
    }
}