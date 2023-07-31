#ifndef GETMAINLIGHTSPARAMS_INCLUDED
#define GETMAINLIGHTSPARAMS_INCLUDED

    #ifndef SHADERGRAPH_PREVIEW
        float4 GetShadowCoord(float3 positionWS)
        {
            float4 shadowCoord;
            float4 positionCS = TransformWorldToHClip(positionWS);
            #if SHADOWS_SCREEN
                shadowCoord = ComputeScreenPos(positionCS);
            #else
                shadowCoord = TransformWorldToShadowCoord(positionWS);
            #endif

            return shadowCoord;
        }
    #endif

    void GetMainLightParams_float(float3 positionWS, out float3 mainLightColor, out float3 mainLightDirection, out float3 mainLightCombinedAttenuation)
    {
        //returns all sorts of info about lights (main and up to 5 additional ones)

        #ifdef SHADERGRAPH_PREVIEW
            // in shadergraph we fake the light
            mainLightColor = float3(1,0.8,0.8);
            mainLightDirection = float3(0.5, 0.5, 0);
            mainLightCombinedAttenuation = 1;

        #else

            float4 shadowCoord = GetShadowCoord(positionWS);

            Light mainLight = GetMainLight(shadowCoord, positionWS, 1);

            //returns
            mainLightColor = mainLight.color;
            mainLightDirection = mainLight.direction;
            mainLightCombinedAttenuation = mainLight.shadowAttenuation*mainLight.distanceAttenuation;                       
        #endif
    }

#endif


