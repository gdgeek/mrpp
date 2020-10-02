using GDGeek;
using MrPP.Myth;
using MrPP.UX;
using System.Collections;
using UnityEngine;

namespace MrPP.Project
{
    public class ClientList : ButtonList<HeroData, ClientButton>, IResize, IExecute
    {
        [SerializeField]
        private Vector3 _size;
        public void execute()
        {
            resize(_size);
        }

        public void resize(Vector3 size)
        {

            this.transform.position = new Vector3(size.x / 2f, 0, 0);
        }
    }
}