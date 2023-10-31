import BpmnJS from 'bpmn-js/lib/Modeler';
import './viewer.js';

window.loadBpmn = (currentInstance) => {

    var canv = document.querySelector('#bpmn-canvas');

    if (canv.hasChildNodes())
        return;

    window.modeler = new BpmnJS({
        container: '#bpmn-canvas'
    });

    window.modeler.on('element.click', async (e) => {
        await currentInstance.invokeMethodAsync('OnElementClick', e.element, e.element.businessObject.sourceRef);
    });


    try {
        window.modeler.createDiagram();
    } catch (err) {
        console.log('error rendering', err);
    }
}

window.getCurrentDiagram = async () => {
    if (!window.modeler) return null;
    var xml = (await window.modeler.saveXML({ format: true })).xml
    return xml;
}

window.setCurrentDiagram = async (xml) => {
    if (!window.modeler) return;

    await window.modeler.importXML(xml);

    const elementRegistry = window.modeler.get("elementRegistry").getAll();
    const modeling = window.modeler.get("modeling");

    const colorsMap = [
        { type: "bpmn:StartEvent",       color: "#2eb85c" },
        { type: "bpmn:UserTask",         color: "#52BAE8" },
        { type: "bpmn:ServiceTask",      color: "#E4F6FF" },
        { type: "bpmn:SendTask",         color: "#FFF3C3" },
        { type: "bpmn:ExclusiveGateway", color: "#FFBA52" },
        { type: "bpmn:EndEvent",         color: "red"  },
    ]

    for (let item of colorsMap) {
        const elements = elementRegistry.filter(p => p.type == item.type);
        modeling.setColor(elements, { stroke: '#222222', fill: item.color })
    }
}



