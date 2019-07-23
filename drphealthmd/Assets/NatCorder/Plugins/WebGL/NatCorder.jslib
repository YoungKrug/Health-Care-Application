const NatCorderWebGL={$sharedInstance:{recordingCallback:null,framebuffer:null,framebufferContext:null,pixelBuffer:null,audioContext:null,audioStream:null,recorder:null,isRecording:!1,MIME_TYPE:"video/webm"},NCInitialize:function(e,n,a){sharedInstance.recordingCallback=n},NCIsRecording:function(){return sharedInstance.isRecording},NCStartRecording:function(e,n,a,r,t,s,o,c){if(1!==e)return void console.log("NatCorder Error: NatCorder WebGL only supports VP8/WebM container");console.log("NatCorder: Starting recording with resolution",n+"x"+a),sharedInstance.framebuffer=document.createElement("canvas"),sharedInstance.framebuffer.width=n,sharedInstance.framebuffer.height=a,sharedInstance.framebufferContext=sharedInstance.framebuffer.getContext("2d"),sharedInstance.pixelBuffer=sharedInstance.framebufferContext.getImageData(0,0,n,a);const d=[sharedInstance.framebuffer.captureStream(r).getVideoTracks()[0]];o>0&&(sharedInstance.audioContext=new AudioContext({latencyHint:"interactive",sampleRate:o}),sharedInstance.audioStream=sharedInstance.audioContext.createMediaStreamDestination({channelCount:c,channelCountMode:"explicit"}),d.push(sharedInstance.audioStream.stream.getAudioTracks()[0]));const i={mimeType:sharedInstance.MIME_TYPE,videoBitsPerSecond:t};sharedInstance.recorder=new MediaRecorder(new MediaStream(d),i),sharedInstance.recorder.start(),sharedInstance.isRecording=!0},NCStopRecording:function(){console.log("NatCorder: Stopping recording"),sharedInstance.recorder.ondataavailable=function(e){const n=new Blob([e.data],{type:sharedInstance.MIME_TYPE}),a=URL.createObjectURL(n);console.log("NatCorder: Completed recording video",n,"to URL:",a);const r=lengthBytesUTF8(a)+1,t=_malloc(r);stringToUTF8(a,t,r),Runtime.dynCall("vi",sharedInstance.recordingCallback,[t])},sharedInstance.recorder.stop(),sharedInstance.audioContext&&sharedInstance.audioContext.close(),sharedInstance.isRecording=!1,sharedInstance.recorder=null,sharedInstance.framebuffer=null,sharedInstance.framebufferContext=null,sharedInstance.pixelBuffer=null,sharedInstance.audioContext=null},NCEncodeFrame:function(e,n){sharedInstance.pixelBuffer.data.set(new Uint8ClampedArray(HEAPU8.buffer,e,sharedInstance.pixelBuffer.width*sharedInstance.pixelBuffer.height*4)),sharedInstance.framebufferContext.putImageData(sharedInstance.pixelBuffer,0,0)},NCEncodeSamples:function(e,n,a){const r=sharedInstance.audioContext.createBuffer(sharedInstance.channelCount,n/sharedInstance.channelCount,sharedInstance.sampleRate);e=new Float32Array(HEAPU8.buffer,e,n);for(var t=0;t<r.numberOfChannels;t++){const n=r.getChannelData(t);for(var s=0;s<r.length;s++)n[s]=e[s*r.numberOfChannels+t]}var o=sharedInstance.audioContext.createBufferSource();o.buffer=r,o.connect(sharedInstance.audioStream),o.start()},NCCurrentTimestamp:function(){return Math.round(1e6*performance.now())}};autoAddDeps(NatCorderWebGL,"$sharedInstance"),mergeInto(LibraryManager.library,NatCorderWebGL);