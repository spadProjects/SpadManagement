CKEDITOR.editorConfig = function (config) {

    config.skin = 'bootstrapck';
    config.language = 'fa';
    config.fontSize_style = {
        styles: { 'font-family': 'tahoma' }
    };

    config.extraPlugins = 'placeholder,fakeobjects,autosave,templates,slideshow';
    config.contentsLangDirection = 'rtl';

    var t=$(this.element.$);
    var ticket = t.attr('tiket');
    var jsbid = t.closest('.bocrud-container').attr('id').substring(2);
    var b = window.bocruds[jsbid];
    b.pcall(t, ticket, null, function (response) {
        var r = eval('(' + response + ')');
        config.placeholderOptions = r;
    }, {async:true}, false, false);

    config.height = 500;

  



};