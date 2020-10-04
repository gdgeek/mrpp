using Mirror.Discovery;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MrPP.Helper;
using Microsoft.MixedReality.Toolkit.Utilities;
using GDGeek;
using MrPP.UX;

namespace MrPP.Project
{
    public class HostList : ButtonList<ServerResponse, HostButton>, IResize, IExecute
    {

        [SerializeField]
        private GridObjectCollection _collection;
        [SerializeField]
        private Vector3 _size;
        public void execute()
        {
            resize(_size);
        }

        public void resize(Vector3 size)
        {
            _collection.CellHeight = _size.y;
            _collection.transform.position = new Vector3(0, -_size.y/2, 0);


        }
    }
}