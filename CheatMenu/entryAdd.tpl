{include file='documentHeader'}

<head>
	<title>{lang}wsif.entry.{@$action}{/lang} - {$category->title|language} - {PAGE_TITLE|language}</title>

	{include file='headInclude'}

	<script data-relocate="true">
		//<![CDATA[
		$(function() {
			WCF.Language.addObject({
				'wcf.label.none': '{lang}wcf.label.none{/lang}'
			});

			new WCF.Label.Chooser({ {implode from=$labelIDs key=groupID item=labelID}{@$groupID}: {@$labelID}{/implode} }, '#messageContainer');

			WCF.Message.Submit.registerButton('text', $('#messageContainer > .formSubmit > input[type=submit]'));
			new WCF.Message.FormGuard();

			{if $action == 'add'}
				$('#disableEntry').click(function() {
					$('#publishingTimeDiv').toggle();
				});
			{/if}

			WCF.System.Dependency.Manager.register('Redactor_text', function() { new WCF.Message.UserMention('text'); });
		});
		//]]>
	</script>
</head>

<body id="tpl{$templateName|ucfirst}" data-template="{$templateName}" data-application="{$templateNameApplication}">
{include file='header'}

<header class="boxHeadline">
	<h1>{lang}wsif.entry.{@$action}{/lang}</h1>
</header>

{include file='userNotice'}

{if !$category->getPermission('user.canAddEntryWithoutModeration')}
	<p class="info">{lang}wsif.entry.moderation.info{/lang}</p>
{/if}

{include file='formError'}

<form id="messageContainer" class="jsFormGuard" method="post" action="{if $action == 'add'}{link application='wsif' controller='EntryAdd' id=$categoryID}{/link}{else}{link application='wsif' controller='EntryEdit' id=$entryID}{/link}{/if}">
	<div class="container containerPadding marginTop">
		{if $action == 'edit' && $entry->canDelete() && !$entry->isDeleted}
			<fieldset>
				<legend>{lang}wsif.entry.delete{/lang}</legend>

				<dl>
					<dt></dt>
					<dd><label><input type="checkbox" id="deleteEntry" name="deleteEntry" value="1" {if $deleteEntry == 1}checked="checked" {/if}/> {lang}wsif.entry.delete.sure{/lang}</label></dd>
				</dl>

				<dl id="deleteReasonDL"{if $errorField == 'deleteReason'} class="formError"{/if}>
					<dt><label for="deleteReason">{lang}wsif.entry.deleteReason{/lang}</label></dt>
					<dd>
						<textarea rows="3" cols="40" id="deleteReason" name="deleteReason">{$deleteReason}</textarea>
						{if $errorField == 'deleteReason'}
							<small class="innerError">
								{if $errorType == 'censoredWordsFound'}
									{lang}wcf.message.error.censoredWordsFound{/lang}
								{else}
									{lang}wsif.entry.deleteReason.error.{@$errorType}{/lang}
								{/if}
							</small>
						{/if}
					</dd>
				</dl>
			</fieldset>

			<script data-relocate="true">
				//<![CDATA[
				$('#deleteEntry').change(function (event) {
					if ($(this).is(':checked')) {
						$('#deleteReasonDL').show();
					}
					else {
						$('#deleteReasonDL').hide();
					}
				});
				$('#deleteEntry').change();
				//]]>
			</script>
		{/if}

		<fieldset>
			<legend>{lang}wsif.entry.information{/lang}</legend>

			{if !$__wcf->user->userID}
				<dl{if $errorField == 'username'} class="formError"{/if}>
					<dt><label for="username">{lang}wcf.user.username{/lang}</label></dt>
					<dd>
						<input type="text" id="username" name="username" value="{$username}" required="required" class="long" />
						{if $errorField == 'username'}
							<small class="innerError">
								{if $errorType == 'empty'}
									{lang}wcf.global.form.error.empty{/lang}
								{else}
									{lang}wcf.user.username.error.{@$errorType}{/lang}
								{/if}
							</small>
						{/if}
					</dd>
				</dl>
			{/if}

			{include file='messageFormMultilingualism'}

			{if $labelGroups|count}
				{hascontent}
					<dl class="jsOnly{if $errorField == 'labelIDs'} formError{/if}">
						<dt><label>{lang}wcf.label.label{/lang}</label></dt>
						<dd>
							<ul class="labelList">
								{content}
									{foreach from=$labelGroups item=labelGroup}
										{if $labelGroup|count}
											<li class="dropdown labelChooser" id="labelGroup{@$labelGroup->groupID}" data-group-id="{@$labelGroup->groupID}" data-force-selection="{if $labelGroup->forceSelection}true{else}false{/if}">
												<div class="dropdownToggle" data-toggle="labelGroup{@$labelGroup->groupID}"><span class="badge label">{lang}wcf.label.none{/lang}</span></div>
												<div class="dropdownMenu">
													<ul class="scrollableDropdownMenu">
														{foreach from=$labelGroup item=label}
															<li data-label-id="{@$label->labelID}"><span><span class="badge label{if $label->cssClassName} {@$label->cssClassName}{/if}">{lang}{$label->label}{/lang}</span></span></li>
														{/foreach}
													</ul>
												</div>
											</li>
										{/if}
									{/foreach}
								{/content}
							</ul>
							{if $errorField == 'labelIDs'}
								<small class="innerError">{lang}wcf.label.error.notValid{/lang}</small>
							{/if}
						</dd>
					</dl>
				{/hascontent}
			{/if}

			<dl{if $errorField == 'subject'} class="formError"{/if}>
				<dt><label for="subject">{lang}wsif.entry.subject{/lang}</label></dt>
				<dd>
					<input type="text" id="subject" name="subject" value="{$subject}" required="required" maxlength="255" class="long" />
					{if $errorField == 'subject'}
						<small class="innerError">
							{if $errorType == 'empty'}
								{lang}wcf.global.form.error.empty{/lang}
							{elseif $errorType == 'censoredWordsFound'}
								{lang}wcf.message.error.censoredWordsFound{/lang}
							{else}
								{lang}wsif.entry.subject.error.{@$errorType}{/lang}
							{/if}
						</small>
					{/if}
				</dd>
			</dl>

			<dl{if $errorField == 'teaser'} class="formError"{/if}>
				<dt><label for="teaser">{lang}wsif.entry.teaser{/lang}</label></dt>
				<dd>
					<textarea id="teaser" name="teaser" rows="5" cols="40" class="long">{$teaser}</textarea>
					<small>{lang}wsif.entry.teaser.description{/lang}</small>
					{if $errorField == 'teaser'}
						<small class="innerError">
							{if $errorType == 'empty'}
								{lang}wcf.global.form.error.empty{/lang}
							{else}
								{lang}wsif.entry.teaser.error.{@$errorType}{/lang}
							{/if}
						</small>
					{/if}
				</dd>
			</dl>

			{if MODULE_TAGGING && WSIF_ENTRY_ENABLE_TAGS && $category->getPermission('user.canSetTags')}{include file='tagInput'}{/if}

			{if ($action == 'add' || $entry->isDisabled) && $category->getPermission('mod.canEnableEntry')}
				{if $action == 'add'}
					<dl>
						<dt></dt>
						<dd>
							<label><input id="disableEntry" name="disableEntry" type="checkbox" value="1"{if $disableEntry} checked="checked"{/if} /> {lang}wsif.entry.disableEntry{/lang}</label>
						</dd>
					</dl>
				{/if}

				<dl{if $errorField == 'publishingTime'} class="formError"{/if}{if $action == 'add' && !$disableEntry} style="display: none"{/if} id="publishingTimeDiv">
					<dt><label for="publishingTime">{lang}wsif.entry.publishingTime{/lang}</label></dt>
					<dd>
						<input type="datetime" id="publishingTime" name="publishingTime" value="{$publishingTime}" class="medium" />
						{if $errorField == 'publishingTime'}
							<small class="innerError">
								{if $errorType == 'empty'}
									{lang}wcf.global.form.error.empty{/lang}
								{else}
									{lang}wsif.entry.publishingTime.error.{@$errorType}{/lang}
								{/if}
							</small>
						{/if}
						<small>{lang}wsif.entry.publishingTime.description{/lang}</small>
					</dd>
				</dl>
			{/if}

			{event name='informationFields'}
		</fieldset>

		{hascontent}
			<fieldset>
				<legend>{lang}wsif.entry.additionalInformation{/lang}</legend>

				{content}
					{foreach from=$fields key='fieldID' item='field'}
						<dl{if $errorField == 'field' && $errorType.fieldID == $field->fieldID} class="formError"{/if}>
							<dt><label for="field{@$fieldID}">{$field}</label></dt>
							<dd>
								<input type="text" id="field{@$fieldID}" name="fieldValues[{@$fieldID}]" value="{$fieldValues[$field->fieldID]}"{if $field->required} required="required"{/if} maxlength="255" class="long" />
								{if $errorField == 'field' && $errorType.fieldID == $field->fieldID}
									<small class="innerError">
										{if $errorType.errorType == 'empty'}
											{lang}wcf.global.form.error.empty{/lang}
										{else}
											{lang}wsif.entry.field.error.{@$errorType.errorType}{/lang}
										{/if}
									</small>
								{/if}
							</dd>
						</dl>
					{/foreach}

					{event name='additionalInformationFields'}
				{/content}
			</fieldset>
		{/hascontent}

		<fieldset>
			<legend>{lang}wsif.entry.contents{/lang}</legend>

			<div class="tabMenuContainer">
				<nav class="tabMenu jsOnly">
					<ul>
						<li id="filesTab"><a href="{@$__wcf->getAnchor('files')}" title="{lang}wsif.entry.files{/lang}">{lang}wsif.entry.files{/lang}</a></li>
						<li id="imagesTab"><a href="{@$__wcf->getAnchor('images')}" title="{lang}wsif.entry.images{/lang}">{lang}wsif.entry.images{/lang}</a></li>
						{event name='tabMenuTabs'}
					</ul>
				</nav>

				{include file='entryAddFiles' application='wsif'}
				{include file='entryAddImages' application='wsif'}

				{event name='tabMenuContents'}
			</div>
		</fieldset>

		<script data-relocate="true">
			//<![CDATA[
			$(function() {
				WCF.TabMenu.init();
			});
			//]]>
		</script>

		{include file='captcha'}

		<fieldset>
			<legend>{lang}wsif.entry.message{/lang}</legend>

			<dl class="wide{if $errorField == 'text'} formError{/if}">
				<dt><label for="text">{lang}wsif.entry.message{/lang}</label></dt>
				<dd>
					<textarea id="text" name="text" rows="20" cols="40" data-autosave="com.wcfsolutions.wsif.entry{if $action == 'add'}Add-{@$category->categoryID}{else}Edit-{@$entry->entryID}{/if}">{$text}</textarea>
					{if $errorField == 'text'}
						<small class="innerError">
							{if $errorType == 'empty'}
								{lang}wcf.global.form.error.empty{/lang}
							{elseif $errorType == 'tooLong'}
								{lang}wcf.message.error.tooLong{/lang}
							{elseif $errorType == 'censoredWordsFound'}
								{lang}wcf.message.error.censoredWordsFound{/lang}
							{elseif $errorType == 'disallowedBBCodes'}
								{lang}wcf.message.error.disallowedBBCodes{/lang}
							{else}
								{lang}wsif.entry.message.error.{@$errorType}{/lang}
							{/if}
						</small>
					{/if}
				</dd>
			</dl>

			{event name='messageFields'}
		</fieldset>

		{include file='messageFormTabs' wysiwygContainerID='text'}

		{event name='fieldsets'}
	</div>

	<div class="formSubmit">
		<input type="submit" value="{lang}wcf.global.button.submit{/lang}" accesskey="s" />
		<input type="hidden" name="tmpHash" value="{$tmpHash}" />
		{include file='messageFormPreviewButton'}
		{@SECURITY_TOKEN_INPUT_TAG}
	</div>
</form>

{include file='footer'}
{include file='wysiwyg'}

</body>
</html>