#ifndef GETADDITIONALLIGHTPARAMS_INCLUDED
#define GETADDITIONALLIGHTPARAMS_INCLUDED



    void GetAdditionalLightParams_float(float3 positionWS, int additionalLightID, out float3 additionalLightColor, out float3 additionalLightDirection, out float3 additionalLightCombinedAttenuation)
    {
        //returns all sorts of info about lights (main and up to 5 additional ones)

        #ifdef SHADERGRAPH_PREVIEW
            // in shadergraph we fake the light

            float previewModifier = (float) additionalLightID / 10;
            additionalLightColor = float3(0.5 + previewModifier,0.5,1 - previewModifier);;
            additionalLightDirection = float3(-0.5+previewModifier, 0.5, 0);
            additionalLightCombinedAttenuation = 1;

        #else

            // float4 shadowCoord = GetShadowCoord(positionWS);

            // Light mainLight = GetMainLight(shadowCoord, positionWS, 1);

            // //returns
            // mainLightColor = mainLight.color;
            // mainLightDirection = mainLight.direction;
            // mainLightCombinedAttenuation = mainLight.shadowAttenuation*mainLight.distanceAttenuation;

            Light additionalLight = GetAdditionalLight(additionalLightID, positionWS, 1);

            additionalLightColor = additionalLight.color;
            additionalLightDirection = additionalLight.direction;
            additionalLightCombinedAttenuation = additionalLight.shadowAttenuation * additionalLight.distanceAttenuation;                        
        #endif
    }

#endif


