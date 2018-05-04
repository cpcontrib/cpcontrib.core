//!packer:combine
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;
using CrownPeak.CMSAPI.Services;

namespace CPContrib.Core
{
	using CrownPeak.CMSAPI;

	public interface ITemplate_Output
	{

		void OnOutput(Asset asset, OutputContext context);
	}

	public interface ITemplate_Input
	{
		void OnInput(Asset asset, InputContext context);
	}

	public interface ITemplate_PostInput
	{
		void OnPostInput(Asset asset, PostInputContext context);

	}

	public interface ITemplate_PostSave
	{
		void OnPostSave(Asset asset, PostSaveContext context);
	}
	
	public interface ITemplate_PostPublish
	{
		void OnPostPublish(Asset asset, PostPublishContext context);
	}
	
}
