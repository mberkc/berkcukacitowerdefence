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
        monster.OnDestroy += RemoveTarget;
        monster.OnFinish  += RemoveTarget;
    }

    public void RemoveTarget() {
        Monster monster = monsterQueue.Dequeue();
        monster.OnDestroy -= RemoveTarget;
        monster.OnFinish  -= RemoveTarget;
        OnQueueUpdate?.Invoke(monsterQueue);
    }
}