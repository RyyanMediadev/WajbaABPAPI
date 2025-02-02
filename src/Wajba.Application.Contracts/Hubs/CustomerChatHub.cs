//using Fos_EF.ChatService;
//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Collections.Concurrent;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FosAPI.Hubs
//{
//    public class CustomerChatHub : Hub
//    {
//        private readonly IChatService _chatService;
//        private readonly FosDBContext _context;

//        // Constructor to inject services
//        public CustomerChatHub(IChatService chatService, FosDBContext context)
//        {
//            _chatService = chatService;
//            _context = context;
//        }

//        // Store connections in a dictionary to map userId to connectionId
//        private static readonly ConcurrentDictionary<string, (string connectionId, string role)> UserConnections = new();

//        // Handle new connections and map the userId and role to the connectionId
//        public override Task OnConnectedAsync()
//        {
//            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();
//            var role = Context.GetHttpContext().Request.Query["role"].ToString();

//            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
//            {
//                // Log an error if userId or role is missing
//                Console.WriteLine("userId or role is missing or invalid.");
//                return Task.CompletedTask;
//            }

//            // Add the connectionId to the dictionary with userId and role
//            UserConnections[userId] = (Context.ConnectionId, role);
//            Console.WriteLine($"User {userId} with role {role} connected with ConnectionId: {Context.ConnectionId}");

//            return base.OnConnectedAsync();
//        }

//        // Handle disconnections and remove the userId from the connections dictionary
//        public override Task OnDisconnectedAsync(Exception? exception)
//        {
//            var userId = UserConnections.FirstOrDefault(x => x.Value.connectionId == Context.ConnectionId).Key;

//            if (!string.IsNullOrEmpty(userId))
//            {
//                UserConnections.TryRemove(userId, out _);
//                Console.WriteLine($"User {userId} disconnected.");
//            }

//            return base.OnDisconnectedAsync(exception);
//        }

//        // Send message from sender to receiver
//        public async Task SendMessage(string senderId, string senderRole, string receiverId, string receiverRole, string message)
//        {
//            if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId) || string.IsNullOrEmpty(message))
//            {
//                Console.WriteLine("Invalid message data.");
//                return;
//            }

//            var chatMessage = new ChatMessage
//            {
//                SenderId = senderId,
//                ReceiverId = receiverId,
//                Message = message,
//                SenderRole = senderRole,
//                ReceiverRole = receiverRole,
//                Timestamp = DateTime.UtcNow
//            };

//            // Save message to the database
//            await _chatService.SaveMessageAsync(chatMessage);

//            // Check if the receiver is connected and send the message
//            if (UserConnections.TryGetValue(receiverId, out var connectionInfo))
//            {
//                await Clients.Client(connectionInfo.connectionId).SendAsync("ReceiveMessage", chatMessage);
//                Console.WriteLine($"Message sent from {senderId} to {receiverId}: {message}");
//            }
//            else
//            {
//                Console.WriteLine($"Receiver {receiverId} is not connected.");
//            }
//        }

//        // Notify receiver that the sender is typing
//        public async Task NotifyTyping(string senderId, string receiverId, bool isTyping)
//        {
//            if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId))
//            {
//                Console.WriteLine("Invalid user information for typing notification.");
//                return;
//            }

//            // Check if receiver is connected
//            if (UserConnections.TryGetValue(receiverId, out var connectionInfo))
//            {
//                await Clients.Client(connectionInfo.connectionId).SendAsync("TypingNotification", senderId, isTyping);
//                Console.WriteLine($"User {senderId} is typing: {isTyping}");
//            }
//            else
//            {
//                Console.WriteLine($"Receiver {receiverId} is not connected.");
//            }
//        }

//        // Mark a message as read and notify the sender
//        public async Task MarkAsRead(int messageId, string readerId)
//        {
//            if (messageId <= 0 || string.IsNullOrEmpty(readerId))
//            {
//                Console.WriteLine("Invalid message or user information.");
//                return;
//            }

//            // Update the read receipt in the database
//            var message = await _chatService.MarkMessageAsReadAsync(messageId);
//            if (message != null)
//            {
//                message.ReadAt = DateTime.UtcNow;
//                await _context.SaveChangesAsync();

//                Console.WriteLine($"Message {messageId} marked as read by {readerId}");
//            }
//            else
//            {
//                Console.WriteLine($"Message {messageId} not found.");
//            }

//            // Notify the sender that the message was read
//            if (UserConnections.TryGetValue(readerId, out var connectionInfo))
//            {
//                await Clients.Client(connectionInfo.connectionId).SendAsync("MessageRead", messageId);
//            }
//            else
//            {
//                Console.WriteLine($"User {readerId} is not connected to receive read receipt.");
//            }
//        }
//    }
//}
