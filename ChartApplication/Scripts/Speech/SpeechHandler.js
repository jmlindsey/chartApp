
function SpeechHandler(textProcessor) {
    if ('webkitSpeechRecognition' in window) {


        var self = this;
        this.textProcessor = textProcessor;
        this.recognition = new webkitSpeechRecognition || SpeechRecognition;
        //var speechRecognitionList = new webkitSpeechGrammarList || SpeechGrammarList;
        ////
        //var grammar = '#JSGF V1.0; grammar colors; public <color> = aqua | azure | beige | bisque | black | blue | brown | chocolate | coral | crimson | cyan | fuchsia | ghostwhite | gold | goldenrod | gray | green | indigo | ivory | khaki | lavender | lime | linen | magenta | maroon | moccasin | navy | olive | orange | orchid | peru | pink | plum | purple | red | salmon | sienna | silver | snow | tan | teal | thistle | tomato | turquoise | violet | white | yellow ;'
        //speechRecognitionList.addFromString(grammar, 1);
        //this.recognition.grammars = speechRecognitionList;
        //

        this.hasResult = false;
        this.result = "";
        this.final_transcript = '';
        this.recognizing = false;
        this.recognition.lang = 'en-US';
        this.recognition.interimResults = true;
        this.recognition.maxAlternatives = 1;
        //this.recognition.onstart = function () {};
        //this.recognition.onaudiostart = function (event) { console.log('onaudiostart'); };
        //this.recognition.onaudioend = function (event) { console.log('onaudioend'); };
        //this.recognition.onspeechend = function () { console.log('Speech has stopped being detected'); };
        //this.recognition.onerror = function (event) { console.log(event.error); };
        this.recognition.onend = function (event) {
            recognizing = false;
            //console.log('onend');
            self.startDictation(event);
        };

        this.recognition.onresult = function (event) {
            var interim_transcript = '';

            for (var i = event.resultIndex; i < event.results.length; ++i) {
                if (event.results[i].isFinal) {
                    final_transcript += event.results[i][0].transcript;
                    self.textProcessor.process(final_transcript);
                } else {
                    interim_transcript += event.results[i][0].transcript;
                }
            }
        };

        this.startDictation = function (event) {
            console.log("started");
            final_transcript = '';
            try {
                self.recognition.start();
            }
            catch (err) { console.log(err); }
        };
    }
}










