using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selene.DataTypes;
using NLua;
using KCelestialBody = global::CelestialBody;

namespace SeleneKSP.DataTypes
{
    class CelestialBody : ICelestialBody
    {
        protected KCelestialBody body;
        protected KSPDataProvider dataProvider;

        public CelestialBody(KSPDataProvider provider, KCelestialBody toUse)
        {
            dataProvider = provider;
            body = toUse;                       
        }

        public double radius { get { return body.Radius; } }
        public double mass { get { return body.Mass; } }
        public double gravity { get { return body.gravParameter; } }
        public double atmosphereThickness { get { return body.maxAtmosphereAltitude; } }
        public double rotationPeriod { get { return body.rotationPeriod; } }
        public double sphereOfInfluence { get { return body.sphereOfInfluence; } }

        public IOrbitInfo orbit
        {
            get { return new OrbitInfo(dataProvider, body.orbit); }
        }
        public ICelestialBody referenceBody
        {
            get
            {
                if (body.referenceBody != null)
                    return new CelestialBody(dataProvider, body.referenceBody);
                else
                    return null;
            }
        }

        public LuaTable GetOrbitingBodies()
        {
            LuaTable toReturn = dataProvider.GetNewTable();
            int num = 1;
            foreach (KCelestialBody b in body.orbitingBodies)
            {
                CelestialBody newBody = new CelestialBody(dataProvider, b);
                toReturn[num++] = newBody;
            }
            return toReturn;
        }
        public double GetLat(SeleneVector wpos)
        {
            return body.GetLatitude(wpos.Vector3D);
        }
        public double GetLon(SeleneVector wpos)
        {
            return body.GetLongitude(wpos.Vector3D);
        }
        public SeleneVector GetLatLonPos(double lat, double lon)
        {
            return body.GetWorldSurfacePosition(lat, lon, 0);
        }

        public SeleneVector GetPosition()
        {
            return body.position;
        }
        public SeleneQuaternion GetRotation()
        {
            return body.transform.rotation;
        }
        public string GetName()
        {
            return body.name;
        }

        public SeleneVector WorldToLocal(SeleneVector toTransform)
        {
            return new SeleneVector(UnityEngine.QuaternionD.Inverse((UnityEngine.QuaternionD)body.transform.rotation) * toTransform);
        }
        public SeleneVector LocalToWorld(SeleneVector toTransform)
        {
            return new SeleneVector(((UnityEngine.QuaternionD)body.transform.rotation) * toTransform);
        }
    }
}
