import BpmnJS from 'bpmn-js/lib/NavigatedViewer';
import ModelingModule from 'bpmn-js/lib/features/modeling';
import TokenSimulationModule from 'bpmn-js-token-simulation/lib/viewer';
import SimulationSupportModule from 'bpmn-js-token-simulation/lib/simulation-support/SimulationSupport';

const FixModule = {

    simulationSupport: ['type', SimulationSupportModule]
}

window.loadBpmnViewer = () => {

    var canv = document.querySelector('#bpmn-canvas');

    if (canv.hasChildNodes())
        return;

    window.viewer = new BpmnJS({
        container: '#bpmn-canvas',
        additionalModules: [
            TokenSimulationModule,
            FixModule,
            ModelingModule
        ]
    });

    try
    {
        window.viewer.createDiagram();
    }
    catch (err)
    {
        console.log('error rendering', err);
    }
}

window.clearColorsViewer = (color) => {

    const elementRegistry = window.viewer.get("elementRegistry").getAll();
    const modeling = window.viewer.get("modeling");

    modeling.setColor(elementRegistry, { stroke: '#222222', fill: color })
}

window.setColorViewer = (elementId, color) => {

    const elementRegistry = window.viewer.get("elementRegistry").getAll();
    const modeling = window.viewer.get("modeling");
    const element = elementRegistry.filter(p => p.id == elementId);

    modeling.setColor(element, { stroke: '#222222', fill: color })
}

window.setEndEventColorViewer = (color) => {

    const elementRegistry = window.viewer.get("elementRegistry").getAll();
    const modeling = window.viewer.get("modeling");
    const endEvent = elementRegistry.filter(p => p.type == "bpmn:EndEvent");

    if (endEvent)
        modeling.setColor(endEvent, { stroke: '#222222', fill: color })
}

window.setCurrentDiagramViewer = async (xml) => {
    if (!window.viewer) return;

    await window.viewer.importXML(xml);
    window.viewer.get('canvas').zoom('fit-viewport');

    if (window.viewer.get('zoomScroll')._enabled)
        window.viewer.get('zoomScroll').toggle();

}



