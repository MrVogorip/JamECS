using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JamECS.Base
{
    public class EcsFilter
    {
        private readonly EcsWorld _world;
        private Expression<Func<EcsEntity, bool>> _expression = _ => true;

        public EcsFilter(EcsWorld world) =>
            _world = world;

        public EcsFilter Add<TComponent>() where TComponent : class, IEcsComponent
        {
            _expression = ToolsExpression.Merge(_expression, x => x.Components<TComponent>().Count > 0);
            return this;
        }

        public IEnumerable<EcsEntity> End() =>
            _world.Entities.Where(_expression.Compile());
    }

    public static class ToolsExpression
    {
        public static Expression<Func<T, bool>> Merge<T>(
            Expression<Func<T, bool>> expressionFrom,
            Expression<Func<T, bool>> expressionTo)
        {
            var swapper = new Swapper(expressionFrom.Parameters.First(), expressionTo.Parameters.First());

            return Expression
                .Lambda<Func<T, bool>>(Expression
                    .AndAlso(swapper.Visit(expressionFrom.Body), expressionTo.Body), expressionTo.Parameters);
        }

        private class Swapper : ExpressionVisitor
        {
            private readonly Expression _expressionFrom;
            private readonly Expression _expressionTo;

            public Swapper(Expression expressionFrom, Expression expressionTo)
            {
                _expressionFrom = expressionFrom;
                _expressionTo = expressionTo;
            }

            public override Expression Visit(Expression expressionBody) =>
                expressionBody == _expressionFrom ? _expressionTo : base.Visit(expressionBody);
        }
    }
}