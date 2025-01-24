﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Audio Events/Composite")]
public class CompositeAudioEvent : AudioEvent
{
    [Serializable]
    public struct CompositeEntry
    {
        public AudioEvent Event;
        public float Weight;
    }

    public CompositeEntry[] Entries;

    public override void Play(Vector3 position = new Vector3())
    {
        float totalWeight = 0;
        for (int i = 0; i < Entries.Length; ++i)
            totalWeight += Entries[i].Weight;

        float pick = Random.Range(0, totalWeight);
        for (int i = 0; i < Entries.Length; ++i)
        {
            if (pick > Entries[i].Weight)
            {
                pick -= Entries[i].Weight;
                continue;
            }

            Entries[i].Event.Play(position);
            return;
        }
    }

    public override void Play(Transform parent)
    {
        float totalWeight = 0;
        for (int i = 0; i < Entries.Length; ++i)
            totalWeight += Entries[i].Weight;

        float pick = Random.Range(0, totalWeight);
        for (int i = 0; i < Entries.Length; ++i)
        {
            if (pick > Entries[i].Weight)
            {
                pick -= Entries[i].Weight;
                continue;
            }

            Entries[i].Event.Play(parent);
            return;
        }
    }

    public override void Play(AudioSource source)
    {
        float totalWeight = 0;
        for (int i = 0; i < Entries.Length; ++i)
            totalWeight += Entries[i].Weight;

        float pick = Random.Range(0, totalWeight);
        for (int i = 0; i < Entries.Length; ++i)
        {
            if (pick > Entries[i].Weight)
            {
                pick -= Entries[i].Weight;
                continue;
            }

            Entries[i].Event.Play(source);
            return;
        }
    }

    public override void PlatUISound()
    {
        Play();
    }
}