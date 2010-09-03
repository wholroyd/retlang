using System;
using System.Collections.Generic;
using Retlang.Core;
using Retlang.Fibers;

namespace Retlang.Channels
{
    /// <summary>
    /// Channel subscription methods.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISubscriber<T>
    {
        ///<summary>
        /// Subscribe to messages on this channel. The provided action will be invoked via a Action on the provided executor.
        ///</summary>
        ///<param name="fiber">the target executor to receive the message</param>
        ///<param name="receive"></param>
        ///<returns>Unsubscriber object</returns>
        IUnsubscriber Subscribe(IFiber fiber, Action<T> receive);

        /// <summary>
        /// Subscribes to events on the channel in batch form. The events will be batched if the consumer is unable to process the events 
        /// faster than the arrival rate.
        /// </summary>
        /// <param name="fiber">The target context to execute the action</param>
        /// <param name="receive"></param>
        /// <param name="intervalInMs">Time in Ms to batch events. If 0 events will be delivered as fast as consumer can process</param>
        /// <returns></returns>
        IUnsubscriber SubscribeToBatch(IFiber fiber, Action<IList<T>> receive, int intervalInMs);

        ///<summary>
        /// Batches events based upon keyed values allowing for duplicates to be dropped. 
        ///</summary>
        ///<param name="fiber"></param>
        ///<param name="keyResolver"></param>
        ///<param name="receive"></param>
        ///<param name="intervalInMs"></param>
        ///<typeparam name="K"></typeparam>
        ///<returns></returns>
        IUnsubscriber SubscribeToKeyedBatch<K>(IFiber fiber, Converter<T, K> keyResolver, Action<IDictionary<K, T>> receive, int intervalInMs);

        /// <summary>
        /// Subscription that delivers the latest message to the consuming thread.  If a newer message arrives before the consuming thread
        /// has a chance to process the message, the pending message is replaced by the newer message. The old message is discarded.
        /// </summary>
        /// <param name="fiber"></param>
        /// <param name="receive"></param>
        /// <param name="intervalInMs"></param>
        /// <returns></returns>
        IUnsubscriber SubscribeToLast(IFiber fiber, Action<T> receive, int intervalInMs);

        /// <summary>
        /// Removes all subscribers.
        /// </summary>
        void ClearSubscribers();
    }
}