using Autodesk.Revit.DB.Analysis;
using VC_Analytical_zones_exporter_Service.Models.SubSurface;

namespace VC_Analytical_zones_exporter_Service.Helpers.ZoneMappers
{
    public class FrameAndDividerMapper
    {
        public static FrameAndDivider MapFrameAndDivider
            (EnergyAnalysisOpening opening)
        {
            string name = opening.Id.ToString() + "_frame";
            string frameWidth = "";
            string frameOutsideProjection = "";
            string frameInsideProjection = "";
            string frameConductance = "";
            string ratioOfFrameEdgeGlassConductanceToCenterOfGlassConductance = "";
            string frameSolarAbsorptance = "";
            string frameVisibleAbsorptance = "";
            string frameThermalHemisphericalEmissivity = "";
            string dividerType = "";
            string dividerWidth = "";
            string numberOfHorizontalDividers = "";
            string numberOfVerticalDividers = "";
            string dividerOutsideProjection = "";
            string dividerInsideProjection = "";
            string dividerConductance = "";
            string ratioOfDividerEdgeGlassConductanceToCenterOfGlassConductance = "";
            string dividerSolarAbsorptance = "";
            string dividerVisibleAbsorptance = "";
            string dividerThermalHemisphericalEmissivity = "";
            string outsideRevealSolarAbsorptance = "";
            string insideSillDepth = "";
            string insideSillSolarAbsorptance = "";
            string insideRevealDepth = "";
            string insideRevealSolarAbsorptance = "";

            FrameAndDivider frameAndDivider = new FrameAndDivider(name, 
                                                                  frameWidth, 
                                                                  frameOutsideProjection, 
                                                                  frameInsideProjection, 
                                                                  frameConductance,
                                                                  ratioOfFrameEdgeGlassConductanceToCenterOfGlassConductance, 
                                                                  frameSolarAbsorptance, 
                                                                  frameVisibleAbsorptance, 
                                                                  frameThermalHemisphericalEmissivity,
                                                                  dividerType, 
                                                                  dividerWidth, 
                                                                  numberOfHorizontalDividers, 
                                                                  numberOfVerticalDividers, 
                                                                  dividerOutsideProjection, 
                                                                  dividerInsideProjection,
                                                                  dividerConductance, 
                                                                  ratioOfDividerEdgeGlassConductanceToCenterOfGlassConductance, 
                                                                  dividerSolarAbsorptance, 
                                                                  dividerVisibleAbsorptance,
                                                                  dividerThermalHemisphericalEmissivity, 
                                                                  outsideRevealSolarAbsorptance, 
                                                                  insideSillDepth, 
                                                                  insideSillSolarAbsorptance,
                                                                  insideRevealDepth, 
                                                                  insideRevealSolarAbsorptance);
            
            return frameAndDivider;
        }

    }
}
