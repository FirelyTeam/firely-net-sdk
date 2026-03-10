## Intro:

Restored the ModelInspector caching behavior removed in previous version.  
We diagnosed the underlying issue, and will now throw a runtime exception in cases where the cross ClassMapping import would occur. 
