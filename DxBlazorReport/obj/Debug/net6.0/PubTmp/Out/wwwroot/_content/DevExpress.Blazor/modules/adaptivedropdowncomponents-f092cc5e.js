import{r as t}from"./dropdowncomponents-782e1f04.js";import{registeredComponents as e}from"./modalcomponents-efc716db.js";import{_ as i}from"./_tslib-6e8ca86b.js";import{d as s}from"./logicaltreehelper-99f1adf9.js";import{e as o}from"./property-d3853089.js";import{e as a}from"./custom-element-267f9a21.js";import{s as r}from"./lit-element-70cf14f4.js";import{d as n}from"./events-interseptor-e0b24870.js";import"./dropdown-844f3190.js";import"./popup-4f623924.js";import"./layouthelper-1d804c8c.js";import"./point-e4ec110e.js";import"./constants-58283e53.js";import"./rafaction-bba7928b.js";import"./transformhelper-ebad0156.js";import"./positiontracker-d7595cd2.js";import"./branch-bf06b0d2.js";import"./capturemanager-89a507c8.js";import"./eventhelper-8570b930.js";import"./dispatcheraction-19309c7b.js";import"./portal-9686dca9.js";import"./data-qa-utils-8be7c726.js";import"./constants-058ebb50.js";import"./dx-html-element-pointer-events-helper-1bf230d1.js";import"./dom-e3fa5208.js";import"./browser-f8f6e902.js";import"./common-eb095e4d.js";import"./devices-9f03a976.js";import"./dx-ui-element-db9e89a3.js";import"./lit-element-base-7a85dca5.js";import"./nameof-factory-64d95f5b.js";import"./custom-events-helper-e7f279d3.js";import"./key-fa0d8a82.js";import"./dx-keyboard-navigator-159220aa.js";import"./focus-utils-6f61e33c.js";import"./touch-167bb2e4.js";import"./disposable-d2c2d283.js";import"./dom-utils-751497ba.js";import"./css-classes-f45f6949.js";import"./thumb-3dd919b6.js";import"./query-44b9267f.js";import"./popupportal-baf3cf8a.js";var p;const d="dxbl-adaptive-dropdown";let l=p=class extends r{constructor(){super(...arguments),this.slotChangedHandler=this.handleSlotChanged.bind(this),this.interceptorSlotChangedHandler=this.handleInterceptorSlotChange.bind(this),this.interceptor=null,this.resizeHandler=this.handleResize.bind(this),this._adaptivityEnabled=!1,this._popupAccessor=null,this.adaptiveWidth=576}get popup(){var t;return(null===(t=this._popupAccessor)||void 0===t?void 0:t.popup)||null}get popupBase(){var t;return(null===(t=this._popupAccessor)||void 0===t?void 0:t.popupBase)||null}get adaptivityEnabled(){return this._adaptivityEnabled}set adaptivityEnabled(t){this._adaptivityEnabled!==t&&(this._adaptivityEnabled=t,this.raiseEnableAdaptivity(t))}createRenderRoot(){const t=super.createRenderRoot(),e=document.createElement("slot");t.appendChild(e),e.addEventListener("slotchange",this.slotChangedHandler);const i=document.createElement("slot");return i.name="interceptor",t.appendChild(i),i.addEventListener("slotchange",this.interceptorSlotChangedHandler),t}connectedCallback(){super.connectedCallback(),window.addEventListener("resize",this.resizeHandler,{passive:!0}),setTimeout((()=>this.updateAdaptivity()))}disconnectedCallback(){super.disconnectedCallback(),window.removeEventListener("resize",this.resizeHandler)}handleResize(t){this.updateAdaptivity()}updateAdaptivity(){this.adaptivityEnabled=this.getActualAdaptivityEnabled()}getActualAdaptivityEnabled(){return window.innerWidth<=this.adaptiveWidth}handleSlotChanged(t){const e=t.target.assignedNodes();this._popupAccessor=p.findPopupAccessor(e)}handleInterceptorSlotChange(t){const e=t.target.assignedNodes();this.interceptor=e[0]}raiseEvent(t,e){var i;null===(i=this.interceptor)||void 0===i||i.raise(t,e)}raiseEnableAdaptivity(t){this.raiseEvent("adaptivityChanged",{enabled:t})}static findPopupAccessor(t){const e=t.find((t=>s(t)));return e||null}};function c(){}i([o({type:Number,attribute:"adaptive-width"})],l.prototype,"adaptiveWidth",void 0),l=p=i([a(d)],l);const m={dropDownRegisteredComponents:t,modalRegisteredComponents:e,init:c,dxAdaptiveDropDownTagName:d,dxEventsInterceptorTagName:n};export{m as default,c as init};