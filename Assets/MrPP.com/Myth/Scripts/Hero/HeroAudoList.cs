using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.Myth
{
    public class HeroAudoList : GDGeek.Singleton<HeroAudoList>, IEnumerable
    {

        public Action<IHero> onAdd{
            get;
            set;
        }


        public Action<IHero> onRemove
        {
            get;
            set;
        }


        public Action<IHero> onRefresh
        {
            get;
            set;
        }
        private HashSet<IHero> _heros = new HashSet<IHero>();
        

        public void add(IHero hero)
        {
            _heros.Add(hero);
            if (onAdd != null) {
                onAdd.Invoke(hero);
            }
        }

        public void remove(IHero hero)
        {
            _heros.Remove(hero);
            if (onRemove != null)
            {
                onRemove.Invoke(hero);
            }
        }

        public void refresh(IHero hero)
        {
            if (_heros.Contains(hero)) {

                if (onRefresh != null)
                {
                    onRefresh.Invoke(hero);
                }
            }
        }
        public bool contains(IHero hero)
        {
            return (_heros.Contains(hero));
        }
        public IHero getHeroById(uint id)
        {
            foreach (IHero hero in _heros)
            {
                if (hero.data.id == id)
                {
                    return hero;
                }
            }
            return null;
        }

        public IHero getHeroByNetId(uint netId)
        {
            foreach (IHero hero in _heros)
            {
                if (hero.netId == netId)
                {
                    return hero;
                }
            }
            return null;
        }

        #region IEnumerable implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public HashSet<IHero>.Enumerator GetEnumerator()
        {
            return _heros.GetEnumerator();
        }

        public IHero getServer()
        {
            Debug.Log("_heros" + _heros.Count);
            foreach(IHero hero in _heros) {
                if (hero.data.isServer) {
                    return hero;
                }
            }
            Debug.Log(" no server");
            return null;
        }


        #endregion
    }
}