using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour 
{
    private readonly Dictionary<string, IService> services = new Dictionary<string, IService>();

    private static ServiceLocator instance;
    public static ServiceLocator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ServiceLocator").AddComponent<ServiceLocator>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    public void Register<Service>(Service concrete) where Service : IService
    {
        string key = typeof(Service).Name;
        if (services.ContainsKey(key))
        {
            Debug.LogError($"SERVICE LOCATOR ERROR\nService of type {key} already registered");
            return;
        }
        services.Add(key, concrete);
    }

    public void Unregister<Service>(Service concrete) where Service : IService
    {
        string key = typeof(Service).Name;
        if (!services.ContainsKey(key))
        {
            Debug.LogError($"SERVICE LOCATOR ERROR\nService of type {key} doesn't exist in locator for unregister it");
            return;
        }
        services.Remove(key);
    }

    public Service Get<Service>() where Service : IService 
    {
        string key = typeof(Service).Name;
        if(!services.ContainsKey(key))
        {
            Debug.LogError($"SERVICE LOCATOR ERROR\nService of type {key} doesn't exist in locator for get it");
            throw new InvalidOperationException();
        }
        return (Service)services[key];
    }
}
