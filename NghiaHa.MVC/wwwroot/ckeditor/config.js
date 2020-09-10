/**
 * @license Copyright (c) 2003-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
    config.height = 600;
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.languages = 'vi';
    config.filebrowserBrowseUrl = '/Content/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Content/ckfinder/ckfinder.html?Types=Images';
    config.filebrowserFlashBrowseUrl = '/Content/ckfinder/ckfinder.html?Types=Flash';
    config.filebrowserUploadUrl = '/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=File';
    config.filebrowserImageUploadUrl = '/Content/Data';
    config.filebrowserFlashUploadUrl = '/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    CKFinder.setupCKEditor(null, '/Content/ckfinder/');
};
