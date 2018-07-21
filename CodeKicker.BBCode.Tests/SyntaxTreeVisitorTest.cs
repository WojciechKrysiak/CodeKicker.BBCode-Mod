using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CodeKicker.BBCode.SyntaxTree;
#if false
using Microsoft.Pex.Framework;
#endif
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests2;

namespace CodeKicker.BBCode.Tests
{
    [TestClass]
#if false
    [PexClass(typeof(SyntaxTreeVisitor), MaxRuns = 1000000000, MaxRunsWithoutNewTests = 1000000000, Timeout = 1000000000, MaxExecutionTreeNodes = 1000000000, MaxBranches = 1000000000, MaxWorkingSet = 1000000000, MaxConstraintSolverMemory = 1000000000, MaxStack = 1000000000, MaxConditions = 1000000000)]
#endif
	public partial class SyntaxTreeVisitorTest
    {
#if false
        [PexMethod]
        public void DefaultVisitorModifiesNothing()
        {
            var tree = BBCodeTestUtil.GetAnyTree();
            var tree2 = new SyntaxTreeVisitor().Visit(tree);
            Assert.IsTrue(ReferenceEquals(tree, tree2));
        }

        [PexMethod]
        public void IdentityModifiedTreesAreEqual()
        {
            var tree = BBCodeTestUtil.GetAnyTree();
            var tree2 = new IdentitiyModificationSyntaxTreeVisitor().Visit(tree);
            Assert.IsTrue(tree == tree2);
        }

        [PexMethod]
        public void TextModifiedTreesAreNotEqual()
        {
            var tree = BBCodeTestUtil.GetAnyTree();
            var tree2 = new TextModificationSyntaxTreeVisitor().Visit(tree);
            Assert.IsTrue(tree != tree2);
        }

		class IdentitiyModificationSyntaxTreeVisitor : SyntaxTreeVisitor
        {
            protected internal override SyntaxTreeNode Visit(TextNode node)
            {
                if (!PexChoose.Value<bool>("x")) return base.Visit(node);

                return new TextNode(node.Text, node.HtmlTemplate);
            }
            protected internal override SyntaxTreeNode Visit(SequenceNode node)
            {
                var baseResult = base.Visit(node);
                if (!PexChoose.Value<bool>("x")) return baseResult;
                return baseResult.SetSubNodes(baseResult.SubNodes.ToList());
            }
            protected internal override SyntaxTreeNode Visit(TagNode node)
            {
                var baseResult = base.Visit(node);
                if (!PexChoose.Value<bool>("x")) return baseResult;
                return baseResult.SetSubNodes(baseResult.SubNodes.ToList());
            }
        }
#endif

		class TextModificationSyntaxTreeVisitor : SyntaxTreeVisitor
        {
            protected override SyntaxTreeNode Visit(TextNode node)
            {
                return new TextNode(node.Text + "x", node.HtmlTemplate);
            }
            protected override SyntaxTreeNode Visit(SequenceNode node)
            {
                var baseResult = base.Visit(node);
                return baseResult.SetSubNodes(baseResult.SubNodes.Concat(new[] { new TextNode("y") }));
            }
            protected override SyntaxTreeNode Visit(TagNode node)
            {
                var baseResult = base.Visit(node);
                return baseResult.SetSubNodes(baseResult.SubNodes.Concat(new[] { new TextNode("z") }));
            }
        }
    }
}
