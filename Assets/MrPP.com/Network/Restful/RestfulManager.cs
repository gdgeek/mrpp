using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using GDGeek;
using Models;
//using MrPP.Maker;
using Proyecto26;
using Proyecto26.Common;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
namespace MrPP.Restful {
    public class RestfulManager : GDGeek.Singleton<RestfulManager>
    {
        [SerializeField]
		private bool _offline = false;
		public RequestHelper options(string uri, Dictionary<string, string> parameters)
		{
			RequestHelper op = options(uri);
			op.Params = parameters;
			return op; 
		}
		/*
		public static string GetUrl(string url, string type, string id = null)
		{

			if (string.IsNullOrEmpty(id))
			{

				return url + "/" + type;
			}
			else
			{

				return url + "/" + type + "/" + id;
			}
		}*/
		public static string Uri(string url, string type, string id = null) {

			if (string.IsNullOrEmpty(id)) {

				return url + "/" + type;
			}
			else {

				return url + "/" + type + "/" + id;
            }

        }

		public RequestHelper options<B>(string uri, Dictionary<string, string> parameters, B body)
		{
			

			RequestHelper op = options(uri, parameters);
			op.Body = body;
			return op;
		}
		/*

            {"Items":[{"abbr":"ts","sn":"sdf","target_id":6,"doc_name":"调试","area":2,"column":2,"layer":0,"section":0},{"abbr":"shc","sn":"ads","target_id":7,"doc_name":"水浒传","area":1,"column":1,"layer":0,"section":0}]}
             */

		private void LogMessage(string title, string message)
		{

		    Debug.LogWarning(message);
		}
		
		public RequestHelper options(string uri) {

			RequestHelper op = new RequestHelper
			{
				Uri = uri,
				//Uri = Url.BuildUrl(uri, new Dictionary<string, string> { { "access-token", UserManager.Instance.accessToken} }),
				Headers = new Dictionary<string, string> {
					//{ "Authorization", "Bearer " + UserManager.Instance.accessToken },
					{ "Content-Type", "application/json" }
				},
				EnableDebug = true
			}; 
			return op;

		}
       
		public Task getTask<T>(RequestHelper requestOptions, Action<T> ret)
		{

			
			Task task = new Task();
			bool isOver = false;
			TaskManager.PushFront(task, delegate
			{

				RestClient.Get<T>(requestOptions).Then( res => {
					this.LogMessage("Posts", JsonUtility.ToJson(res));
					ret(res);
					isOver = true;
				}).Catch(delegate(Exception err) {
					this.LogMessage("Error", err.Message);
					isOver = true;
				    }
                );
			});
			TaskManager.AddAndIsOver(task, delegate
			{

				return isOver;
			});
			return task;
		}
		public Task getArrayTask<T>(RequestHelper requestOptions, Action<T[]> ret)
		{


			Task task = new Task();
			bool isOver = false;
			TaskManager.PushFront(task, delegate
			{

				RestClient.GetArray<T>(requestOptions).Then(res => {
					this.LogMessage("Posts", JsonUtility.ToJson(res));
					ret(res);
					isOver = true;
				}).Catch(delegate (Exception err) {
					this.LogMessage("Error", err.Message);
					isOver = true;
				}
				);
			});
			TaskManager.AddAndIsOver(task, delegate
			{
				return isOver;
			});
			return task;
		}

		public void getArray<T>(RequestHelper options, Action<T[]> ret) {  
			RestClient.GetArray<T>(options).Then(res => {
				this.LogMessage("Posts", JsonHelper.ArrayToJsonString<T>(res));
				ret(res);
			}).Catch(err => this.LogMessage("Error", err.Message));
		}
		public void get<T>(RequestHelper options, Action<T> ret)
		{
			string url = options.Uri.BuildUrl(options.Params);
		
			if (Offline() && _offline)
			{
				T t = this.readOffline<T>(url);
				ret(t);
				  
			}
			else {
				RestClient.Get<T>(options).Then(res => {
					this.writeOffline(url, res);
					Debug.Log(res);
					this.LogMessage("Posts", JsonUtility.ToJson(res));
					ret(res);


				}).Catch(err => this.LogMessage("Error", err.Message));
			}
			
		}

        private void writeOffline<T>(string url, T res)
        {
			PlayerPrefs.SetString("@" + url, JsonUtility.ToJson(res));
           
        }

        private T readOffline<T>(string url)
        {
            if(PlayerPrefs.HasKey("@" + url)) { 
                string json = PlayerPrefs.GetString("@" + url);
				return JsonUtility.FromJson<T>(json);
			}
            return default(T);
        }
		
        private static  bool Offline()
        {
			return (Application.internetReachability == NetworkReachability.NotReachable);
		
        }

        public int put<T>(RequestHelper options, Action<T> ret) {


           
			RestClient.Put<T>(options, (err, res, body) => {
				if (err != null)
				{
					this.LogMessage("Error", err.Message);
				}
				else
				{
					ret(body);
					this.LogMessage("Success", JsonUtility.ToJson(body, true));
				}
			});

			return 0;
		
		}

		public int post<T>( RequestHelper requestOptions, Action<T> ret)
		{
            
			RestClient.Post<T>(requestOptions)
			.Then(res => {

			    // And later we can clear the default query string params for all requests
			    RestClient.ClearDefaultParams();

				this.LogMessage("Success", JsonUtility.ToJson(res, true));
				ret(res);
			})
			.Catch(err => this.LogMessage("Error", err.Message));

			return 0;
        }
    }
}